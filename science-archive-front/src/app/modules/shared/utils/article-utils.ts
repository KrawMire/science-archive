import { Article, ArticleAuthor, ArticleDocument } from "@models/article/article";
import { Category } from "@models/category/category";

/**
 * Mirrors ArticleStatus on the backend
 */
export enum ArticleStatus {
  ToVerify = 0,
  Verified = 1,
  Declined = 2,
}

/**
 * Mirrors ArticleAuthorRole on the backend
 */
export enum ArticleAuthorRole {
  Owner = 0,
  Collaborator = 1,
}

export type DocumentKind = "pdf" | "data" | "other";

const DATA_EXTENSIONS = ["csv", "tsv", "xls", "xlsx", "json", "xml", "zip", "tar", "gz", "7z", "rar", "parquet"];
const NEW_ARTICLE_DAYS = 7;

export function documentExtension(document: ArticleDocument): string {
  const dotIndex = document.name.lastIndexOf(".");
  return dotIndex > 0 ? document.name.substring(dotIndex + 1).toLowerCase() : "";
}

export function documentKind(document: ArticleDocument): DocumentKind {
  const extension = documentExtension(document);

  if (extension === "pdf") {
    return "pdf";
  }

  return DATA_EXTENSIONS.includes(extension) ? "data" : "other";
}

export function documentUrl(document: ArticleDocument): string {
  return `/api/documents/${document.path}`;
}

export function hasDocumentOfKind(article: Article, kind: DocumentKind): boolean {
  return article.documents?.some(document => documentKind(document) === kind) ?? false;
}

export function sortedAuthors(article: Article): ArticleAuthor[] {
  return [...(article.authors ?? [])].sort((a, b) => a.role - b.role);
}

export function authorNames(article: Article): string {
  return sortedAuthors(article).map(author => author.name).join(", ");
}

export function initials(name?: string | null): string {
  if (!name) {
    return "";
  }

  return name
    .split(/\s+/)
    .filter(part => part.length > 0)
    .slice(0, 2)
    .map(part => part[0].toUpperCase())
    .join("");
}

export function toDate(value?: Date | string | null): Date | null {
  if (!value) {
    return null;
  }

  const date = value instanceof Date ? value : new Date(value);
  return isNaN(date.getTime()) ? null : date;
}

/**
 * Whether the article is recent enough to carry the rose bookmark
 */
export function isNewArticle(article: Article, now: Date = new Date()): boolean {
  const created = toDate(article.creationDate);

  if (!created) {
    return false;
  }

  return now.getTime() - created.getTime() < NEW_ARTICLE_DAYS * 24 * 60 * 60 * 1000;
}

/**
 * Finds the field (top-level category) the article's subcategory belongs to
 */
export function findField(categories: Category[], subcategoryId?: string | null): Category | undefined {
  if (!subcategoryId) {
    return undefined;
  }

  return categories.find(category => category.subcategories.some(sub => sub.id === subcategoryId));
}

export function buildCitation(article: Article, url: string): string {
  const year = toDate(article.creationDate)?.getFullYear();
  const authors = authorNames(article);
  const parts = [
    authors ? `${authors}${year ? ` (${year})` : ""}.` : year ? `(${year}).` : "",
    `${article.title}.`,
    "Science Archive.",
    url,
  ];

  return parts.filter(part => part.length > 0).join(" ");
}

import { User } from "@models/user/user";

/**
 * Represents response to get all authors from API
 */
export interface GetAllAuthorsResponse {
  /**
   * All existing authors
   */
  authors: User[];
}

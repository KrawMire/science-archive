package repositories

import "io"

type StorageRepository interface {
	GetDocument(filename string) ([]byte, error)
	UploadDocument(file io.Reader, filename string) (string, error)
}

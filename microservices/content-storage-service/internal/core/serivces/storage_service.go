package serivces

import "mime/multipart"

type StorageService interface {
	GetDocument(filename string) ([]byte, error)
	UploadDocument(fileHeader *multipart.FileHeader) (string, error)
}

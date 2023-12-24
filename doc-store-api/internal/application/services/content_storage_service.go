package services

import (
	"errors"
	"github.com/google/uuid"
	"mime/multipart"
	"path/filepath"
	"science-archive/doc-store-api/internal/core/repositories"
)

type ContentStorageService struct {
	storageRepository repositories.StorageRepository
}

func NewContentStorageService(repository repositories.StorageRepository) *ContentStorageService {
	return &ContentStorageService{
		storageRepository: repository,
	}
}

func (s *ContentStorageService) GetDocument(filename string) ([]byte, error) {
	result, err := s.storageRepository.GetDocument(filename)

	if err != nil {
		return nil, err
	}

	return result, nil
}

func (s *ContentStorageService) UploadDocument(fileHeader *multipart.FileHeader) (string, error) {
	id := uuid.New().String()

	file, err := fileHeader.Open()

	if err != nil {
		return "", errors.New("cannot read file")
	}

	fileExtension := filepath.Ext(fileHeader.Filename)

	if fileExtension != ".pdf" {
		return "", errors.New("file can be only in PDF format")
	}

	filename := id + fileExtension

	return s.storageRepository.UploadDocument(file, filename)
}

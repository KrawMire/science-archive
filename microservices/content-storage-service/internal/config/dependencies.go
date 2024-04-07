package config

import (
	"github.com/aws/aws-sdk-go/aws"
	"github.com/aws/aws-sdk-go/aws/credentials"
	"science-archive/content-storage-service/internal/api/handlers"
	"science-archive/content-storage-service/internal/application/services"
	corerepositories "science-archive/content-storage-service/internal/core/repositories"
	coreservices "science-archive/content-storage-service/internal/core/serivces"
	"science-archive/content-storage-service/internal/persistence/object_storage/options"
	"science-archive/content-storage-service/internal/persistence/object_storage/repositories"
)

func ConfigureHandlers() *handlers.StorageHandler {
	service := configureServices()
	return handlers.NewStorageHandler(service)
}

func configureServices() coreservices.StorageService {
	repository := configureRepositories()
	return services.NewContentStorageService(repository)
}

func configureRepositories() corerepositories.StorageRepository {
	keyID, key := GetObjectStorageCredentials()

	connOptions := options.ObjectStorageConnectionOptions{
		Config: aws.Config{
			Endpoint:    aws.String(GetObjectStorageBaseUrl()),
			Region:      aws.String(GetObjectStorageRegion()),
			Credentials: credentials.NewStaticCredentials(keyID, key, ""),
		},
		BucketName: GetBucketName(),
	}

	return repositories.NewObjectStorageRepository(connOptions)
}

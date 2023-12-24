package repositories

import (
	"bytes"
	"github.com/aws/aws-sdk-go/aws"
	"github.com/aws/aws-sdk-go/aws/session"
	"github.com/aws/aws-sdk-go/service/s3"
	"github.com/aws/aws-sdk-go/service/s3/s3manager"
	"io"
	"science-archive/doc-store-api/internal/persistence/object_storage/options"
)

type ObjectStorageRepository struct {
	options options.ObjectStorageConnectionOptions
}

func NewObjectStorageRepository(connOptions options.ObjectStorageConnectionOptions) *ObjectStorageRepository {
	return &ObjectStorageRepository{
		options: connOptions,
	}
}

func (r *ObjectStorageRepository) GetDocument(filename string) ([]byte, error) {
	sess, err := session.NewSession(&r.options.Config)

	if err != nil {
		return nil, err
	}

	client := s3.New(sess)

	input := &s3.GetObjectInput{
		Bucket: aws.String(r.options.BucketName),
		Key:    aws.String(filename),
	}

	result, err := client.GetObject(input)

	if err != nil {
		return nil, err
	}

	defer result.Body.Close()
	buf := new(bytes.Buffer)
	_, err = buf.ReadFrom(result.Body)

	if err != nil {
		return nil, err
	}

	return buf.Bytes(), nil
}

func (r *ObjectStorageRepository) UploadDocument(file io.Reader, filename string) (string, error) {
	sess, err := session.NewSession(&r.options.Config)

	if err != nil {
		return "", err
	}

	uploader := s3manager.NewUploader(sess)

	_, err = uploader.Upload(&s3manager.UploadInput{
		Bucket: aws.String(r.options.BucketName),
		Key:    aws.String(filename),
		Body:   file,
	})

	if err != nil {
		return "", err
	}

	return filename, nil
}

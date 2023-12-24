package handlers

import (
	"github.com/gin-gonic/gin"
	"log"
	"net/http"
	"science-archive/doc-store-api/internal/api/dtos"
	"science-archive/doc-store-api/internal/api/dtos/storage/response_dtos"
	"science-archive/doc-store-api/internal/core/serivces"
)

type StorageHandler struct {
	storageService serivces.StorageService
}

func NewStorageHandler(service serivces.StorageService) *StorageHandler {
	return &StorageHandler{
		storageService: service,
	}
}

func (d *StorageHandler) GetDocument(c *gin.Context) {
	docName := c.Param("docName")

	if docName == "" {
		c.IndentedJSON(400, dtos.Response{Success: false, Error: "Document name is not presented"})
	}

	result, err := d.storageService.GetDocument(docName)

	if err != nil {
		errorStr := "Error occurred while getting document"

		log.Println(errorStr, err)
		c.IndentedJSON(400, dtos.Response{Success: false, Error: errorStr})
		return
	}

	c.Header("Content-Disposition", "inline; filename="+docName)

	c.Data(http.StatusOK, "application/pdf", result)
}

func (d *StorageHandler) UploadDocument(c *gin.Context) {
	fileHeader, err := c.FormFile("document")

	if err != nil || fileHeader == nil {
		errorStr := "File could not be read or not present"

		log.Println(errorStr, err)
		c.IndentedJSON(400, dtos.Response{Success: false, Error: errorStr})
		return
	}

	path, err := d.storageService.UploadDocument(fileHeader)

	if err != nil {
		log.Println("Error while uploading file", err)
		c.IndentedJSON(400, dtos.Response{Success: false, Error: err.Error()})
		return
	}

	resData := &response_dtos.UploadDocumentResponse{
		Path: path,
	}

	res := dtos.Response{
		Success: true,
		Data:    resData,
	}

	c.IndentedJSON(201, res)
}

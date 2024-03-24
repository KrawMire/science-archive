package config

import (
	"github.com/go-yaml/yaml"
	"log"
	"os"
	"path/filepath"
)

const envKey = "LOG_SERVICE_ENV"
const devEnv = "DEV"
const releaseEnv = "RELEASE"

type Config struct {
	Services ServicesConfig `yaml:"services"`
}

type ServicesConfig struct {
	Email EmailConfig `yaml:"email"`
}

type EmailConfig struct {
	Sender   string `yaml:"sender"`
	Login    string `yaml:"login"`
	Password string `yaml:"password"`
	Host     string `yaml:"host"`
	Port     string `yaml:"port"`
}

func GetConfig() *Config {
	var config *Config

	switch env := getAppEnv(); env {
	case devEnv:
		config = getDevConfig()
	case releaseEnv:
		config = getReleaseConfig()
	default:
		log.Fatalf("Invalid app environment: %s", env)
	}

	return config
}

func getAppEnv() string {
	env := os.Getenv(envKey)

	if env == "" {
		env = devEnv
	}

	return env
}

func getDevConfig() *Config {
	path, err := filepath.Abs("../config/config_dev.yml")
	if err != nil {
		log.Fatalf("Cannot get config file path. Error: %s", err)
	}

	fileContent, err := os.ReadFile(path)
	if err != nil {
		log.Fatalf("Cannot read file at path %s. Error: %s", path, err)
	}

	config := &Config{}
	err = yaml.Unmarshal(fileContent, config)

	return config
}

func getReleaseConfig() *Config {
	return nil
}

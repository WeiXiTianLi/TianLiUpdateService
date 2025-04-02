# TianLiUpdateService

## Overview

TianLiUpdateService is a web service for managing project updates. It has been re-implemented in Rust using Actix-web for building web APIs and Diesel for database operations. The service is configured to use SQLite as the database.

## API Endpoints

### Projects

#### POST
- /projects
- /projects/{name}/version

#### GET
- /projects/{name}
- /projects/{name}/version
- /projects/{name}/list
- /projects/{name}/download_url
- /projects/{name}/hash
- /projects/{name}/download_url_and_hash

### Tokens

#### POST
- /tokens/create_token_string

## Data Models

### Project
| Name | Type |
| - | - |
| id | string($uuid) |
| name | string |

### ProjectVersion
| Name | Type |
| - | - |
| id | string($uuid) |
| project_id | string($uuid) |
| version | string |
| description | string |
| download_url | string |
| hash | string |
| update_log | string |
| create_time | string($date-time) |

### Token
| Name | Type |
| - | - |
| id | string($uuid) |
| token_string | string |
| last_use_time | string($date-time) |

## Running the Service

### Prerequisites
- Rust
- Docker

### Building and Running

1. Build the Docker image:
   ```sh
   docker build -t tianli.update-service .
   ```

2. Run the Docker container:
   ```sh
   docker run -d -p 34072:80 --name tianli.update_service tianli.update-service
   ```

3. The service will be available at `http://localhost:34072`.

## Configuration

The service uses a configuration file `config.toml` to set up the database connection and other settings. Here is an example configuration:

```toml
[database]
url = "sqlite://database/tianli_project.db"
```

# 天理更新服务


### Projects

##### POST
- /Projects
- /Projects/\{name\}/Version

##### GET
- /Projects/Names
- /Projects/\{name\}/Versions
- /Projects/\{name\}/\{version\}
- /Projects/\{name\}/LatestVersion
- /Projects/\{name\}/LatestVersion/DownloadUrl
- /Projects/\{name\}/LatestVersion/Hash
- /Projects/\{name\}/LatestVersion/DownloadUrlAndHash
### Tokens

##### POST
- /Tokens/CreateTokenString

## Schemas
### ProjectVersion
| Name | Type |
| - | - |
| projectVersionID  |  string($uuid)      |
| projectItemID	 |  string($uuid)         |
| version	 | string                     |
|   - nullable:  | true                   |
| description	 | string                 |
|   - nullable:  | true                   |
| downloadUrl	 | string                 |
|   - nullable:  | true                   |
| hash	 | string                         |
|   - nullable: |  true                   |
| updateLog | 	string                    |
|   - nullable:  | true                   |
| createTime	 | string($date-time)     |
| create_TokenId	 | string($uuid)      |

## Token
| Name | Type |
| - | - |
| tokenID	 | string($uuid)             |
| tokenString	 | string                |
|   - nullable:  | true                  |
| lastUseTime	 | string($date-time)    |

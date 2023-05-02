# 天理更新服务


### Projects

##### POST
- [x] /Projects
- [x] \{name\}/Version

##### GET
- [x] \{name\}
- [x] \{name\}/Version
- [x] \{name\}/list
- [x] \{name\}/DownloadUrl
- [x] \{name\}/Hash
- [x] \{name\}/DownloadUrlAndHash

##### PUT
- [x] \{name\}/Version

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

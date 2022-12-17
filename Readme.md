# 天理更新服务


### Projects

##### POST
- [ ] /Projects
- [x] \{name\}/Version

##### GET
- [ ] /Projects/Names
- [ ] /Projects/\{name\}/Versions
- [x] \{name\}/\{version\}
- [x] \{name\}/Version
- [x] \{name\}/DownloadUrl
- [x] \{name\}/Hash
- [x] \{name\}/DownloadUrlAndHash
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

use serde::{Deserialize, Serialize};
use diesel::prelude::*;
use diesel::sqlite::SqliteConnection;
use diesel::sql_types::Text;
use crate::schema::{projects, project_versions, files, tokens};

#[derive(Queryable, Insertable, Serialize, Deserialize)]
#[table_name = "projects"]
pub struct Project {
    pub id: String,
    pub name: String,
}

#[derive(Queryable, Insertable, Serialize, Deserialize)]
#[table_name = "project_versions"]
pub struct ProjectVersion {
    pub id: String,
    pub project_id: String,
    pub version: String,
    pub description: String,
    pub download_url: String,
    pub hash: String,
    pub update_log: String,
    pub create_time: String,
}

#[derive(Queryable, Insertable, Serialize, Deserialize)]
#[table_name = "files"]
pub struct File {
    pub id: String,
    pub project_version_id: String,
    pub file_name: String,
    pub file_path: String,
    pub download_url: String,
    pub hash: String,
}

#[derive(Queryable, Insertable, Serialize, Deserialize)]
#[table_name = "tokens"]
pub struct Token {
    pub id: String,
    pub token_string: String,
    pub last_use_time: String,
}

use actix_web::{web, HttpResponse};
use diesel::prelude::*;
use serde::Deserialize;

use crate::models::{Project, ProjectVersion, Token};
use crate::schema::{projects, project_versions, tokens};
use crate::establish_connection;

#[derive(Deserialize)]
struct AddProjectRequest {
    project_name: String,
}

#[derive(Deserialize)]
struct AddVersionRequest {
    version: String,
    download_url: String,
    hash: String,
    update_log: String,
}

#[derive(Deserialize)]
struct CreateTokenRequest {
    su_token: String,
}

pub fn init(cfg: &mut web::ServiceConfig) {
    cfg.service(
        web::scope("")
            .route("/projects", web::post().to(add_project))
            .route("/projects/{name}/version", web::post().to(add_version))
            .route("/projects/{name}", web::get().to(get_project))
            .route("/projects/{name}/version", web::get().to(get_latest_version))
            .route("/projects/{name}/list", web::get().to(get_version_list))
            .route("/projects/{name}/download_url", web::get().to(get_download_url))
            .route("/projects/{name}/hash", web::get().to(get_hash))
            .route("/projects/{name}/download_url_and_hash", web::get().to(get_download_url_and_hash))
            .route("/tokens/create_token_string", web::post().to(create_token)),
    );
}

async fn add_project(req: web::Json<AddProjectRequest>) -> HttpResponse {
    let conn = establish_connection();
    let new_project = Project {
        id: uuid::Uuid::new_v4().to_string(),
        name: req.project_name.clone(),
    };

    diesel::insert_into(projects::table)
        .values(&new_project)
        .execute(&conn)
        .expect("Error adding new project");

    HttpResponse::Ok().json(new_project)
}

async fn add_version(
    web::Path(name): web::Path<String>,
    req: web::Json<AddVersionRequest>,
) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let new_version = ProjectVersion {
        id: uuid::Uuid::new_v4().to_string(),
        project_id: project.id.clone(),
        version: req.version.clone(),
        description: "".to_string(),
        download_url: req.download_url.clone(),
        hash: req.hash.clone(),
        update_log: req.update_log.clone(),
        create_time: chrono::Utc::now().to_string(),
    };

    diesel::insert_into(project_versions::table)
        .values(&new_version)
        .execute(&conn)
        .expect("Error adding new version");

    HttpResponse::Ok().json(new_version)
}

async fn get_project(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    HttpResponse::Ok().json(project)
}

async fn get_latest_version(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let version = project_versions::table
        .filter(project_versions::project_id.eq(project.id.clone()))
        .order(project_versions::create_time.desc())
        .first::<ProjectVersion>(&conn)
        .expect("Error finding latest version");

    HttpResponse::Ok().json(version)
}

async fn get_version_list(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let versions = project_versions::table
        .filter(project_versions::project_id.eq(project.id.clone()))
        .load::<ProjectVersion>(&conn)
        .expect("Error finding versions");

    HttpResponse::Ok().json(versions)
}

async fn get_download_url(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let version = project_versions::table
        .filter(project_versions::project_id.eq(project.id.clone()))
        .order(project_versions::create_time.desc())
        .first::<ProjectVersion>(&conn)
        .expect("Error finding latest version");

    HttpResponse::Ok().json(version.download_url)
}

async fn get_hash(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let version = project_versions::table
        .filter(project_versions::project_id.eq(project.id.clone()))
        .order(project_versions::create_time.desc())
        .first::<ProjectVersion>(&conn)
        .expect("Error finding latest version");

    HttpResponse::Ok().json(version.hash)
}

async fn get_download_url_and_hash(web::Path(name): web::Path<String>) -> HttpResponse {
    let conn = establish_connection();
    let project = projects::table
        .filter(projects::name.eq(name.clone()))
        .first::<Project>(&conn)
        .expect("Error finding project");

    let version = project_versions::table
        .filter(project_versions::project_id.eq(project.id.clone()))
        .order(project_versions::create_time.desc())
        .first::<ProjectVersion>(&conn)
        .expect("Error finding latest version");

    HttpResponse::Ok().json(format!("{}|{}", version.hash, version.download_url))
}

async fn create_token(req: web::Json<CreateTokenRequest>) -> HttpResponse {
    let conn = establish_connection();
    let su_token = "super_secret_token"; // Replace with your actual super token

    if req.su_token != su_token {
        return HttpResponse::Unauthorized().finish();
    }

    let new_token = Token {
        id: uuid::Uuid::new_v4().to_string(),
        token_string: uuid::Uuid::new_v4().to_string(),
        last_use_time: chrono::Utc::now().to_string(),
    };

    diesel::insert_into(tokens::table)
        .values(&new_token)
        .execute(&conn)
        .expect("Error creating token");

    HttpResponse::Ok().json(new_token.token_string)
}

table! {
    projects (id) {
        id -> Text,
        name -> Text,
    }
}

table! {
    project_versions (id) {
        id -> Text,
        project_id -> Text,
        version -> Text,
        description -> Text,
        download_url -> Text,
        hash -> Text,
        update_log -> Text,
        create_time -> Text,
    }
}

table! {
    files (id) {
        id -> Text,
        project_version_id -> Text,
        file_name -> Text,
        file_path -> Text,
        download_url -> Text,
        hash -> Text,
    }
}

table! {
    tokens (id) {
        id -> Text,
        token_string -> Text,
        last_use_time -> Text,
    }
}

joinable!(project_versions -> projects (project_id));
joinable!(files -> project_versions (project_version_id));

allow_tables_to_appear_in_same_query!(
    projects,
    project_versions,
    files,
    tokens,
);

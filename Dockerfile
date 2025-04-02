# Use the official Rust image as the base image
FROM rust:latest AS builder

# Set the working directory
WORKDIR /app

# Copy the Cargo.toml and Cargo.lock files
COPY Cargo.toml Cargo.lock ./

# Create a dummy main.rs file to build the dependencies
RUN mkdir src && echo "fn main() {}" > src/main.rs

# Build the dependencies
RUN cargo build --release && rm -rf src

# Copy the source code
COPY . .

# Build the application
RUN cargo build --release

# Use a minimal image for the final stage
FROM debian:buster-slim

# Set the working directory
WORKDIR /app

# Copy the compiled binary from the builder stage
COPY --from=builder /app/target/release/tianli_update_service .

# Copy the configuration file
COPY config.toml .

# Expose the port the application will run on
EXPOSE 8080

# Set the entry point to run the application
ENTRYPOINT ["./tianli_update_service"]

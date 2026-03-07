# Security and Health Check Improvements

This document describes the security and health check improvements added to the WebApi project.

## 1. Configuration-Based Security Settings

### Overview
Security settings (HTTPS and CORS) are now governed by `appsettings.json` rather than environment names, allowing flexible configuration across different deployment scenarios.

### Configuration

```json
{
  "Security": {
    "requireHttpsMetadata": false,
    "cors": {
      "allowAnyOrigin": true,
      "allowedOrigins": []
    }
  }
}
```

### Settings Explanation

- **requireHttpsMetadata**: Controls whether HTTPS metadata is required for JWT authentication
  - `false`: For local development and early test environments
  - `true`: For production environments with proper SSL/TLS setup

- **cors.allowAnyOrigin**: Controls CORS policy
  - `true`: Allows any origin (useful for development)
  - `false`: Restricts to specified origins

- **cors.allowedOrigins**: Array of allowed origins (only used when `allowAnyOrigin` is `false`)
  - Example: `["https://your-frontend.com", "https://admin.your-frontend.com"]`

### Production Configuration

See `appsettings.Production.json` for a production-ready configuration example with:
- HTTPS metadata validation enabled
- Restricted CORS origins
- HTTPS endpoints

## 2. Global Exception Middleware

### Overview
A centralized exception handling middleware that catches unhandled exceptions and returns consistent error responses.

### Features

- **Centralized Error Handling**: All unhandled exceptions are caught and logged
- **Consistent Error Responses**: Returns structured JSON error responses
- **Security**: Hides internal error details in production (returns generic message for 500 errors)
- **Structured Logging**: Uses source-generated logging for performance

### Error Response Format

```json
{
  "statusCode": 500,
  "message": "An internal server error occurred. Please try again later.",
  "type": "InvalidOperationException"
}
```

### HTTP Status Code Mapping

- `ArgumentException` → 400 Bad Request
- `UnauthorizedAccessException` → 401 Unauthorized
- `KeyNotFoundException` → 404 Not Found
- `InvalidOperationException` → 409 Conflict
- All other exceptions → 500 Internal Server Error

## 3. Health Checks

### Overview
Health check endpoints to monitor the status of external dependencies.

### Endpoints

- **`/health`**: Overall health status of all dependencies
- **`/health/ready`**: Readiness probe (same as `/health` for now)

### Monitored Services

1. **KurrentDB**: Event store database
2. **MongoDB**: Read model database
3. **RabbitMQ**: Message broker

### Response Format

**Healthy:**
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "kurrent": {
      "status": "Healthy",
      "description": "KurrentDB is reachable"
    },
    "mongodb": {
      "status": "Healthy",
      "description": "MongoDB is reachable"
    },
    "rabbitmq": {
      "status": "Healthy",
      "description": "RabbitMQ is reachable"
    }
  }
}
```

**Unhealthy:**
```json
{
  "status": "Unhealthy",
  "entries": {
    "mongodb": {
      "status": "Unhealthy",
      "description": "MongoDB is not reachable",
      "exception": "..."
    }
  }
}
```

### Health Check Implementation

Each health check:
- Tests connectivity to the respective service
- Logs failures for monitoring
- Returns appropriate health status

## Migration Guide

### For Existing Deployments

1. **Add Security Configuration** to your `appsettings.json`:
   ```json
   {
     "Security": {
       "requireHttpsMetadata": false,
       "cors": {
         "allowAnyOrigin": true,
         "allowedOrigins": []
       }
     }
   }
   ```

2. **Update for Production**:
   - Set `requireHttpsMetadata: true`
   - Set `allowAnyOrigin: false`
   - Specify allowed origins in `allowedOrigins` array

3. **Health Check Monitoring**:
   - Configure your orchestrator (Kubernetes, Docker Swarm, etc.) to use `/health` endpoint
   - Set up alerts for unhealthy status

### For Kubernetes

```yaml
livenessProbe:
  httpGet:
    path: /health
    port: 8080
  initialDelaySeconds: 30
  periodSeconds: 10

readinessProbe:
  httpGet:
    path: /health/ready
    port: 8080
  initialDelaySeconds: 10
  periodSeconds: 5
```

## Testing

### Health Check Testing

```bash
# Check all services
curl http://localhost:5000/health

# Detailed status
curl http://localhost:5000/health | jq
```

### Exception Middleware Testing

```bash
# Should return structured error response
curl -X POST http://localhost:5000/api/invalid-endpoint
```

## Files Added/Modified

### New Files
- `src/WebApi/Security/SecuritySettings.cs` - Security configuration model
- `src/WebApi/Middleware/GlobalExceptionMiddleware.cs` - Exception handling middleware
- `src/WebApi/Middleware/GlobalExceptionMiddlewareLogs.cs` - Source-generated logging
- `src/WebApi/HealthChecks/KurrentHealthCheck.cs` - KurrentDB health check
- `src/WebApi/HealthChecks/KurrentHealthCheckLogs.cs` - Logging
- `src/WebApi/HealthChecks/MongoHealthCheck.cs` - MongoDB health check
- `src/WebApi/HealthChecks/MongoHealthCheckLogs.cs` - Logging
- `src/WebApi/HealthChecks/RabbitMqHealthCheck.cs` - RabbitMQ health check
- `src/WebApi/HealthChecks/RabbitMqHealthCheckLogs.cs` - Logging
- `src/WebApi/appsettings.Production.json` - Production configuration example

### Modified Files
- `src/WebApi/Program.cs` - Integrated new middleware and health checks
- `src/WebApi/appsettings.json` - Added Security section
- `src/WebApi/WebApi.csproj` - Added required packages

## Dependencies Added

- `Microsoft.Extensions.Diagnostics.HealthChecks` - Built-in health check framework
- `RabbitMQ.Client` - For RabbitMQ health check connectivity

## Performance Considerations

- Health checks are lightweight and use minimal resources
- Exception middleware has negligible performance impact
- Source-generated logging provides zero-allocation logging

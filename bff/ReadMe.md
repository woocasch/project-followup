# BFF for Project Follow-Up application

## Logging guidelines

Logs are sent to OTEL collector using structured logging. Use `LoggerMessage` attribute to define the log message template and log level.

Use following ranges for event IDs:
| Range from | Range to | Description |
| --- | --- | --- |
| 1 | 1000 | CQRS infrastructure logs |
| 1001 | 2000 | Event Bus logs |
| 2001 | 3000 | Event sourcing logs |
| 3001 | 4000 | Users logs |
| 4001 | 5000 | Projects logs |
| 5001 | 6000 | Tasks logs |
| 6001 | 7000 | Activation links logs |
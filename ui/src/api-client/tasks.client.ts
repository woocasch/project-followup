import { ApiClientBase } from './api-client-base';
import type * as model from './tasks.model';

export class TasksClient extends ApiClientBase implements model.TasksApi {
    async createTask(parameters: model.CreateTaskParameters): Promise<model.CreateTaskResult> {
        const url = `api/projects/${parameters.projectId}/tasks`;
        const payload = parameters.taskDetails;
        const response = await this.createBffClient().post(url, payload);
        if (response.status === 201) {
            return response.data;
        }

        throw new Error('Failed to create task');
    }
}

const tasksApi: model.TasksApi = new TasksClient();

export default tasksApi;
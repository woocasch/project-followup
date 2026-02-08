import type * as model from './create-task.model';
import tasksApi from '@apiClient/tasks.client';
import * as taskApiModel from '@apiClient/tasks.model';

export class CreateTaskWebService implements model.CreateTaskService {
  async createTask(
    request: model.CreateTaskRequest,
  ): Promise<model.CreateTaskResponse> {
    const parameters: taskApiModel.CreateTaskParameters = {
      projectId: request.projectId,
      taskDetails: {
        title: request.taskDetails.title,
        description: request.taskDetails.description,
        dueDate: request.taskDetails.dueDate
          ? new Date(request.taskDetails.dueDate)
          : undefined,
      },
    };
    const result = await tasksApi.createTask(parameters);
    return {
      taskId: result.taskId,
    };
  }
}

const createTaskService = new CreateTaskWebService();

export default createTaskService;

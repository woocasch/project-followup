import tasksApi from '@apiClient/tasks.client';
import type * as taskApiModel from '@apiClient/tasks.model';
import type * as model from './create-task.model';

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
    try {
      const result = await tasksApi.createTask(parameters);
      return {
        taskId: result.taskId,
      };
    } catch (error) {
      console.error('Error creating task:', error);
      return {};
    }
  }
}

const createTaskService = new CreateTaskWebService();

export default createTaskService;

import type * as model from './create-task.model';

export class CreateTaskWebService implements model.CreateTaskService {
  async createTask(
    _: model.CreateTaskRequest,
  ): Promise<model.CreateTaskResponse> {
    return new Promise((resolve) => {
      setTimeout(() => {
        console.log('Creating task with data:', _);
        resolve({ taskId: '' });
      }, 1000);
    });
  }
}

const createTaskService = new CreateTaskWebService();

export default createTaskService;

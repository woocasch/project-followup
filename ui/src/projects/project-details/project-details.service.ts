import projectsApi from '@apiClient/projects.client';
import * as apiModel from '@apiClient/projects.model';
import type * as model from './project-details.model';

const taskStatusMapping: Record<apiModel.ProjectTaskStatus, string> = {
  [apiModel.ProjectTaskStatus.Created]: 'created',
  [apiModel.ProjectTaskStatus.InProgress]: 'in progress',
  [apiModel.ProjectTaskStatus.Completed]: 'completed',
  [apiModel.ProjectTaskStatus.Removed]: 'removed',
};

function mapTaskStatus(status: apiModel.ProjectTaskStatus): string {
  return taskStatusMapping[status] || 'Unknown';
}

export class ProjectDetailsWebService implements model.ProjectDetailsService {
  async getProject(
    request: model.GetProjectRequest,
  ): Promise<model.GetProjectResponse> {
    const response = await projectsApi.getProject({
      projectId: request.projectId,
    });

    if (!response.project) {
      return { project: null };
    }

    const projectDetails: model.ProjectDetails = {
      id: response.project.id,
      title: response.project.title,
      description: response.project.description,
    };
    return { project: projectDetails };
  }

  async fetchProjectUsers(
    request: model.FetchProjectUsersRequest,
  ): Promise<model.FetchProjectUsersResponse> {
    const response = await projectsApi.fetchProjectUsers({
      projectId: request.projectId,
    });
    return response;
  }

  async fetchProjectTasks(
    request: model.FetchProjectTasksRequest,
  ): Promise<model.FetchProjectTasksResponse> {
    const response = await projectsApi.fetchProjectTasks({
      projectId: request.projectId,
    });
    const tasks: model.TaskData[] = response.tasks.map((task) => ({
      id: task.id,
      title: task.title,
      status: mapTaskStatus(task.status),
      rawStatus: task.status,
    }));
    return { tasks };
  }
}

const projectDetailsService: model.ProjectDetailsService =
  new ProjectDetailsWebService();

export default projectDetailsService;

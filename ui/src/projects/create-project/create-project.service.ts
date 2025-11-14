import projectsApi from '@apiClient/projects.client';
import type * as model from './create-project.model';

export class CreateProjectWebService implements model.CreateProjectService {
  async createProject(
    request: model.CreateProjectRequest,
  ): Promise<model.CreateProjectResponse> {
    const response = await projectsApi.create({
      title: request.title,
      description: request.description,
    });

    const success = response;
    return { success };
  }
}

const createProjectService: model.CreateProjectService =
  new CreateProjectWebService();

export default createProjectService;

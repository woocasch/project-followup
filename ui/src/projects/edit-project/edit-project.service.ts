import projectsApi from '@apiClient/projects.client';
import type * as model from './edit-project.model';

export class EditProjectWebService implements model.EditProjectService {
  async getProject(
    request: model.GetProjectRequest): Promise<model.GetProjectResponse> {
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

  async editProject(
    request: model.EditProjectRequest,
  ): Promise<model.EditProjectResponse> {
    const response = await projectsApi.edit({
      projectId: request.projectId,
      title: request.title,
      description: request.description,
    });

    const success = response;
    return { success };
  }
}   

const editProjectService: model.EditProjectService =
  new EditProjectWebService();

export default editProjectService;

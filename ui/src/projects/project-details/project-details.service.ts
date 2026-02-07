import type * as model from './project-details.model';
import projectsApi from '@apiClient/projects.client';

export class ProjectDetailsWebService implements model.ProjectDetailsService {
    async getProject(request: model.GetProjectRequest): Promise<model.GetProjectResponse> {
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

    async fetchProjectUsers(request: model.FetchProjectUsersRequest): Promise<model.FetchProjectUsersResponse> {
        const response = await projectsApi.fetchProjectUsers({
            projectId: request.projectId,
        });
        return response;
    }

    async fetchProjectTasks(request: model.FetchProjectTasksRequest): Promise<model.FetchProjectTasksResponse> {
        const response = await projectsApi.fetchProjectTasks({
            projectId: request.projectId,
        });
        return response;
    }
}

const projectDetailsService: model.ProjectDetailsService = new ProjectDetailsWebService();

export default projectDetailsService;
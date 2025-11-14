import projectsApi from '@apiClient/projects.client';
import type * as model from './projects-list.model';

export class ProjectsListWebService implements model.ProjectsListService {
  async getProjectsList(): Promise<model.ProjectListItem[]> {
    const result = await projectsApi.fetchProjectsList();
    if (result?.projects) {
      return result.projects.map((p) => {
        return {
          id: p.id,
          name: p.title,
          description: p.description,
          membersCount: p.membersCount,
        };
      });
    }

    return [];
  }
}

const projectsListService: model.ProjectsListService =
  new ProjectsListWebService();

export default projectsListService;

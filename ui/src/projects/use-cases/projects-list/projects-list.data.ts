import { projectsApi } from '@apiClient/projects.client';

export interface ProjectRole {
  id: string;
  name: string;
}

export interface ProjectMember {
  id: string;
  name: string;
  roles: ProjectRole[];
}

export interface ProjectListItem {
  id: string;
  name: string;
  description: string;
  membersCount: number;
}

export interface ProjectsListService {
  getProjectsList(): Promise<ProjectListItem[]>;
}

export class WebProjectListService implements ProjectsListService {
  async getProjectsList(): Promise<ProjectListItem[]> {
    const result = await projectsApi.fetchProjectsList();
    if (result && result.projects) {
      return result.projects.map(p => {
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

export const projectsListService: ProjectsListService =
  new WebProjectListService();

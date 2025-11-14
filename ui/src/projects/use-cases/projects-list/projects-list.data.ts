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
    // Simulate an API call
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve([
          {
            id: '1',
            name: 'Connect application to logging infrastructure',
            description:
              'Prepare our application to send logs to the central logging system.',
            membersCount: 3,
          },
          {
            id: '2',
            name: 'Migrate nuget packages to .NET Standard 2.0',
            description:
              'Rework packages to be compatible with .NET Standard 2.0 for better cross-platform support. Mark packages that have to be in .NET Framework 4.8 to be decommissioned.',
            membersCount: 5,
          },
        ]);
      }, 1000);
    });
  }
}

export const projectsListService: ProjectsListService =
  new WebProjectListService();

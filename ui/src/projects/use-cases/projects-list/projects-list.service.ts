import type { ProjectListItem } from "./projects-list.data";

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
                        description: 'Prepare our application to send logs to the central logging system.',
                        membersCount: 3,
                        tasksCompleted: 8,
                        tasksTotal: 20,
                    },
                    {
                        id: '2',
                        name: 'Migrate nuget packages to .NET Standard 2.0 and go decomission .NET Framework 4.8 packages',
                        description: 'Rework packages to be compatible with .NET Standard 2.0 for better cross-platform support. Mark packages that have to be in .NET Framework 4.8 to be decomissioned.',
                        membersCount: 5,
                        tasksCompleted: 15,
                        tasksTotal: 30,
                    },
                ]);
            }, 1000);
        });
    }
}

export const projectsListService: ProjectsListService = new WebProjectListService();
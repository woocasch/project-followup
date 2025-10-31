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
                        name: 'Project 1',
                        description: 'Description for Project 1',
                        membersCount: 3,
                    },
                    {
                        id: '2',
                        name: 'Project 2',
                        description: 'Description for Project 2',
                        membersCount: 5,
                    },
                ]);
            }, 1000);
        });
    }
}

export const projectsListService: ProjectsListService = new WebProjectListService();
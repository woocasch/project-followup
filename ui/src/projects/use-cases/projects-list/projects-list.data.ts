export interface ProjectListItem {
    id: string;
    name: string;
    description: string;
    members: ProjectMember[];
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
                        members: [
                            {
                                id: '1',
                                name: 'Member 1',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                            {
                                id: '2',
                                name: 'Member 2',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                            {
                                id: '3',
                                name: 'Member 3',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                        ],
                    },
                    {
                        id: '2',
                        name: 'Project 2',
                        description: 'Description for Project 2',
                        members: [
                            {
                                id: '1',
                                name: 'Member 1',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                            {
                                id: '2',
                                name: 'Member 2',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                            {
                                id: '3',
                                name: 'Member 3',
                                roles: [
                                    { id: '1', name: 'Developer' },
                                    { id: '2', name: 'Designer' },
                                ],
                            },
                        ],
                    },
                ]);
            }, 1000);
        });
    }
}

export const projectsListService: ProjectsListService = new WebProjectListService();

export interface ProjectListItem {
    id: string;
    title: string;
    description: string;
    membersCount: number;
    tasksCompleted: number;
    tasksTotal: number;
}

export interface FetchListResult {
    projects: ProjectListItem[];
}

export interface ProjectsApi {
    fetchProjectsList(): Promise<FetchListResult>;
}
import { useEffect, useState } from 'react';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import * as Data from './projects-list.data';
import { ProjectCardComponent } from './components';
import { PageHeader } from '@components/index';

const ProjectsContainer = styled.div(`
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: ${theme.sizes.spacing.medium};
`);

export default function ProjectsListPage() {
    const [projects, setProjects] = useState<Data.ProjectListItem[]>([]);

    useEffect(() => {
        Data.projectsListService.getProjectsList().then(setProjects);
    }, []);

    return <div>
        <PageHeader>Projects List Page</PageHeader>
        <ProjectsContainer>
            {projects.map(project => (
                <ProjectCardComponent key={project.id} project={project} />
))}
        </ProjectsContainer>
    </div>
}

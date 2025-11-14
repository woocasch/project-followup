import { useEffect, useState } from 'react';
import styled from '@emotion/styled';
import { Button } from '@components/index';
import { theme } from '@root/theme';
import * as Data from './projects-list.data';
import { ProjectCardComponent } from './components';
import { PageHeader } from '@components/index';

const HeaderContainer = styled.div(`
    display: grid;
    grid-template-columns: 1fr auto;
    align-items: center;
    margin-bottom: ${theme.spaces.medium};
`);

const ProjectsContainer = styled.div(`
    display: grid;
    grid-template-columns: repeat(5, 1fr);
    gap: ${theme.spaces.medium};
`);

export default function ProjectsListComponent() {
    const [projects, setProjects] = useState<Data.ProjectListItem[]>([]);

    useEffect(() => {
        Data.projectsListService.getProjectsList().then(setProjects);
    }, []);

    return <div>
        <HeaderContainer>
            <PageHeader>Projects</PageHeader>
            <Button variant='action' buttonType='rounded'>Create project</Button>
        </HeaderContainer>
        <ProjectsContainer>
            {projects.map(project => (
                <div key={project.id}>
                    <ProjectCardComponent project={project} />
                </div>
            ))}
        </ProjectsContainer>
    </div>
}

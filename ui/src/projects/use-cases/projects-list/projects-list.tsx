import { useEffect, useState } from 'react';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import * as Data from './projects-list.data';
import * as Service from './projects-list.service';
import { ProjectCardComponent } from './components';

const ProjectsContainer = styled.div(`
    display: flex;
    flex-wrap: wrap;
    align-items: flex-start;
    gap: ${theme.sizes.spacing.medium};
`);

export default function ProjectsListPage() {
    const [projects, setProjects] = useState<Data.ProjectListItem[]>([]);

    useEffect(() => {
        Service.projectsListService.getProjectsList().then(setProjects);
    }, []);

    return <div>
        <ProjectsContainer>
            {projects.map(project => (
                <ProjectCardComponent key={project.id} project={project} />
))}
        </ProjectsContainer>
    </div>
}

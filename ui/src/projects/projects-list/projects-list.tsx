import { Button, PageHeader } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { ProjectCardComponent } from './components';
import type * as model from './projects-list.model';
import projectsListService from './projects-list.service';

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
    align-items: start;
`);

export default function ProjectsListComponent() {
  const [projects, setProjects] = useState<model.ProjectListItem[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    projectsListService.getProjectsList().then(setProjects);
  }, []);

  function onCreateProjectClick() {
    navigate('/projects/create');
  }

  return (
    <div>
      <HeaderContainer>
        <PageHeader>Projects</PageHeader>
        <Button
          variant="action"
          buttonType="rounded"
          onClick={onCreateProjectClick}
        >
          Create project
        </Button>
      </HeaderContainer>
      <ProjectsContainer>
        {projects.map((project) => (
          <ProjectCardComponent key={project.id} project={project} />
        ))}
      </ProjectsContainer>
    </div>
  );
}

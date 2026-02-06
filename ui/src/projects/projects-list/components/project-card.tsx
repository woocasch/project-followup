import { Card, CardSize } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import {
  ClipboardCheck,
  Pencil,
  Presentation,
  Search,
  User,
} from 'lucide-react';
import { NavLink } from 'react-router';
import type * as model from '../projects-list.model';

interface ProjectCardComponentProps {
  project: model.ProjectListItem;
}

const ProjectHeaderContainer = styled.div(`
    font-size: ${theme.fontSizes.small};
`);

const MembersCount = styled.span(`
    svg {
        height: 0.8em;
    }
`);

const TasksCount = styled.span(`
    svg {
        height: 0.8em;
    }
`);

const ProjectContentContainer = styled.div(`
    font-size: ${theme.fontSizes.medium};
`);

const ButtonsContainer = styled.div(`
    font-size: ${theme.fontSizes.small};
    display: grid;
    grid-template-columns: repeat(3, 1fr) repeat(2, auto);
    gap: ${theme.spaces.xsmall};
    align-items: center;
    text-align: center;
    & > * {
        justify-self: center;
    }

    & > svg {
        height: 1.4em;}
`);

export function ProjectCardComponent({ project }: ProjectCardComponentProps) {
  
  return (
    <Card size={CardSize.Medium}>
      <Card.Header>
        <ProjectHeaderContainer>{project.name}</ProjectHeaderContainer>
      </Card.Header>
      <Card.Content>
        <ProjectContentContainer>{project.description}</ProjectContentContainer>
      </Card.Content>
      <Card.Footer>
        <ButtonsContainer>
          {/* <NavLink to={`/projects/${project.id}/meeting`} title='Start follow-up meeting' aria-label='Start follow-up meeting'>
            <Presentation />
          </NavLink> */}
          <NavLink to={`/projects/${project.id}/details`} title='View project details' aria-label='View project details'>
            <Search />
          </NavLink>
          <NavLink to={`/projects/${project.id}/edit`} title='Edit project' aria-label='Edit project'>
            <Pencil />
          </NavLink>
          <MembersCount>
            <User />
            {project.usersCount}
          </MembersCount>
          <TasksCount>
            <ClipboardCheck />
            {project.tasksCompleted}/{project.tasksTotal}
          </TasksCount>
        </ButtonsContainer>
      </Card.Footer>
    </Card>
  );
}

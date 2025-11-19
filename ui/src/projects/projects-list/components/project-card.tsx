import { Button, Card, CardSize } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { ClipboardCheck, User } from 'lucide-react';
import type * as model from '../projects-list.model';
import { useNavigate } from 'react-router';

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
    grid-template-columns: repeat(4, auto);
    gap: ${theme.spaces.xsmall};
    align-items: center;
`);

export function ProjectCardComponent({ project }: ProjectCardComponentProps) {
  const navigate = useNavigate();
  function onEditClick() {
    navigate(`/projects/${project.id}/edit`);
  }

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
          <Button variant="action">Follow-up meeting</Button>
          <Button variant="action">Project details</Button>
          <Button variant="action" onClick={onEditClick}>Edit project</Button>
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

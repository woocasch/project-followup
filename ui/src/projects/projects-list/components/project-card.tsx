import { Button, IconButton, Card, CardSize } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { ClipboardCheck, Pencil, Presentation, ReceiptText, User } from 'lucide-react';
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
          <IconButton icon={Presentation} />
          <IconButton icon={ReceiptText} />
          <IconButton icon={Pencil}  onClick={onEditClick} />
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

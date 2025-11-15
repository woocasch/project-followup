import { Button, Card, CardSize } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { ClipboardCheck, User } from 'lucide-react';
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
    grid-template-columns: repeat(4, auto);
    gap: ${theme.spaces.xsmall};
    align-items: center;
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
          <Button>Follow-up meeting</Button>
          <Button>Project details</Button>
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

import styled from '@emotion/styled';
import { Button, LoadingSpinner, NamedPanel } from '@root/components';
import { theme } from '@root/theme';
import { useEffect, useState } from 'react';
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';
import * as apiModel from '@apiClient/projects.model';

export interface TasksListProps {
  projectId: string;
}

const taskStatusColors: Record<apiModel.ProjectTaskStatus, string> = {
  [apiModel.ProjectTaskStatus.Created]: theme.colors.surface,
  [apiModel.ProjectTaskStatus.InProgress]: theme.colors.warning,
  [apiModel.ProjectTaskStatus.Completed]: theme.colors.success,
  [apiModel.ProjectTaskStatus.Removed]: theme.colors.error,
};

function mapTaskStatusColor(status: apiModel.ProjectTaskStatus): string {
  return taskStatusColors[status] || theme.colors.surface;
}

const ListStyled = styled.ul(`
    margin: ${theme.spaces.medium};
    list-style: none;
    padding-left: 0;
    max-height: 200px;
    overflow-y: auto;
`);

interface TaskContainerProps {
  status: apiModel.ProjectTaskStatus;
}

const TaskContainer = styled('div')<TaskContainerProps>`
    margin: ${theme.spaces.medium};
    padding: ${theme.spaces.medium};
    background-color: ${(props: TaskContainerProps) => mapTaskStatusColor(props.status)};
    border: 1px solid ${theme.colors.border};
    border-radius: ${theme.borderRadius.medium};
    &>h3 {
        font-size: ${theme.fontSizes.large};
    }
    &>p {
        width: 100%;
        max-width: 100%;
        font-size: ${theme.fontSizes.medium};
    }
`;

const EmptyListMessage = styled.p(`
    margin: ${theme.spaces.medium};
    font-size: ${theme.fontSizes.medium};
`);

function onOpenTaskDetails(task: model.TaskData) {
  alert(`Opening details for task ${task.title} with status ${task.status}`);
}

function displayTask(task: model.TaskData) {
  return (
    <TaskContainer status={task.rawStatus}>
      <h3>{task.title} is {task.status}</h3>
      <div>
        <Button buttonType='rounded' variant='action' title='View task details' onClick={() => onOpenTaskDetails(task)}>Details</Button>
      </div>
    </TaskContainer>
  );
}

function displayTasks(tasks: model.TaskData[]) {
  if (tasks.length === 0) {
    return <EmptyListMessage>No tasks assigned to this project</EmptyListMessage>;
  }

  return (
    <ListStyled>
      {tasks.map((task) => (
        <li key={task.id}>{displayTask(task)}</li>
      ))}
    </ListStyled>
  );
}

export default function TasksList({ projectId }: TasksListProps) {
  const [loadingTasks, setLoadingTasks] = useState(false);
  const [tasks, setTasks] = useState<model.TaskData[]>([]);

  useEffect(() => {
    setLoadingTasks(true);
    setTasks([]);
    const request: model.FetchProjectTasksRequest = {
      projectId: projectId,
    };
    projectDetailsService.fetchProjectTasks(request).then((r) => {
      setTasks(r.tasks);
      setLoadingTasks(false);
    });
  }, [projectId]);

  return (
    <NamedPanel title="Tasks">
      {loadingTasks ? <LoadingSpinner /> : displayTasks(tasks)}
    </NamedPanel>
  );
}

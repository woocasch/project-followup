import styled from '@emotion/styled';
import { LoadingSpinner, NamedPanel } from '@root/components';
import { theme } from '@root/theme';
import { useEffect, useState } from 'react';
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';

export interface TasksListProps {
  projectId: string;
}

const ListStyled = styled.ul(`
    margin: ${theme.spaces.medium};
    list-style: none;
    padding-left: 0;
    max-height: 200px;
    overflow-y: auto;
    &>li {
        inline-width: 100%;
    }
`);

const TaskContainer = styled.div(`
    margin: ${theme.spaces.medium};
    padding: ${theme.spaces.medium};
    background-color: ${theme.colors.surface};
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
`);

function displayTask(task: model.TaskData) {
  return (
    <TaskContainer>
      <h3>{task.title}</h3>
      <p>Status: {task.status}</p>
    </TaskContainer>
  );
}

function displayTasks(tasks: model.TaskData[]) {
  if (tasks.length === 0) {
    return <p>No tasks assigned to this project</p>;
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

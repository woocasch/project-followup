import * as apiModel from '@apiClient/projects.model';
import styled from '@emotion/styled';
import { Button, LoadingSpinner, NamedPanel } from '@root/components';
import { theme } from '@root/theme';
import CreateTask from '@tasks/create-task/create-task';
import { format } from 'date-fns';
import { useCallback, useEffect, useState } from 'react';
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';

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
    list-style: none;
    padding-left: 0;
    overflow-y: auto;
    &>li {
      margin-top: ${theme.spaces.medium};
    }
`);

interface TaskContainerProps {
  status: apiModel.ProjectTaskStatus;
}

const TaskContainer = styled('div')<TaskContainerProps>`
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
    font-size: ${theme.fontSizes.medium};
`);

const ButtonsContainer = styled.div(`
    display: flex;
    gap: ${theme.spaces.medium};
    text-align: left;
`);

const DueDate = styled.div(`
    font-size: ${theme.fontSizes.small};
`);

function onOpenTaskDetails(task: model.TaskData) {
  alert(`Opening details for task ${task.title} with status ${task.status}`);
}

function displayDueDate(dueDate?: Date) {
  if (!dueDate) {
    return null;
  }

  const formattedDate = format(dueDate, 'yyyy-MM-dd');
  return <DueDate>Due date: {formattedDate}</DueDate>;
}

function displayTask(task: model.TaskData) {
  return (
    <TaskContainer status={task.rawStatus}>
      <h3>
        {task.title} is {task.status}
      </h3>
      {displayDueDate(task.dueDate)}
      <div>
        <Button
          buttonType="rounded"
          variant="action"
          title="View task details"
          onClick={() => onOpenTaskDetails(task)}
        >
          Details
        </Button>
      </div>
    </TaskContainer>
  );
}

function displayTasks(tasks: model.TaskData[]) {
  if (tasks.length === 0) {
    return (
      <EmptyListMessage>No tasks assigned to this project</EmptyListMessage>
    );
  }

  return (
    <ListStyled>
      {tasks.map((task) => (
        <li key={task.id}>{displayTask(task)}</li>
      ))}
    </ListStyled>
  );
}

function displayTasksButtons(setIsCreateTaskOpen: (isOpen: boolean) => void) {
  return (
    <ButtonsContainer>
      <Button
        buttonType="rounded"
        variant="action"
        title="Add task"
        onClick={() => setIsCreateTaskOpen(true)}
      >
        Add Task
      </Button>
    </ButtonsContainer>
  );
}

export default function TasksList({ projectId }: TasksListProps) {
  const [loadingTasks, setLoadingTasks] = useState(false);
  const [tasks, setTasks] = useState<model.TaskData[]>([]);
  const [isCreateTaskOpen, setIsCreateTaskOpen] = useState(false);

  function onTaskCreated() {
    loadTasks();
  }

  const loadTasks = useCallback(() => {
    setTasks([]);
    setLoadingTasks(true);
    const request: model.FetchProjectTasksRequest = {
      projectId: projectId,
    };
    projectDetailsService.fetchProjectTasks(request).then((r) => {
      setTasks(r.tasks);
      setLoadingTasks(false);
    });
  }, [projectId]);

  useEffect(() => {
    loadTasks();
  }, [loadTasks]);

  return (
    <NamedPanel title="Tasks">
      {displayTasksButtons(setIsCreateTaskOpen)}
      {loadingTasks ? <LoadingSpinner /> : displayTasks(tasks)}
      <CreateTask
        projectId={projectId}
        isOpen={isCreateTaskOpen}
        setIsOpen={setIsCreateTaskOpen}
        onTaskCreated={onTaskCreated}
      />
    </NamedPanel>
  );
}

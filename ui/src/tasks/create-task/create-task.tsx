import styled from '@emotion/styled';
import { zodResolver } from '@hookform/resolvers/zod';
import {
  Button,
  DateInput,
  MultilineText,
  PageHeader,
  Text,
} from '@root/components';
import { DialogBox } from '@root/components/dialog-box';
import { theme } from '@root/theme';
import { useLayoutEffect, useRef } from 'react';
import { useForm } from 'react-hook-form';
import createTaskLogic from './create-task.logic';
import type * as model from './create-task.model';
import createTaskService from './create-task.service';

export interface CreateTaskProps {
  projectId: string;
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
  onTaskCreated: () => void;
}

const FormContainer = styled.div(`
    display: grid;
    width: 30em;
    grid-template-columns: auto 2fr;
    gap: ${theme.spaces.medium};
    &>.buttons, &>div>.error {
        grid-column: span 2;
        text-align: center;
    }

    & label {
      text-align: right;
    }
    
    & textarea {
      min-height: 5em;
    }
`);

export default function CreateTask(props: CreateTaskProps) {
  const { projectId, isOpen, setIsOpen, onTaskCreated } = props;
  const dialogRef = useRef<HTMLDialogElement>(null);
  const form = useForm({
    resolver: zodResolver(createTaskLogic.getFormSchema()),
  });

  useLayoutEffect(() => {
    if (!dialogRef.current) {
      return;
    }

    if (dialogRef.current.open && !isOpen) {
      dialogRef.current.close();
    } else if (!dialogRef.current.open && isOpen) {
      form.reset({
        title: '',
        description: '',
        dueDate: undefined,
      });
      dialogRef.current.showModal();
    }
  }, [isOpen, form.reset]);

  function onDialogClosed() {
    if (isOpen) {
      setIsOpen(false);
    }
  }

  async function onSubmit(data: model.NewTaskData): Promise<void> {
    const request: model.CreateTaskRequest = {
      projectId: projectId,
      taskDetails: data,
    };

    const response = await createTaskService.createTask(request);
    if (!response?.taskId) {
      console.error('Failed to create task');
      // Maybe toastr here some day? For now, just log the error.
      return;
    }

    setIsOpen(false);
    onTaskCreated();
  }

  return (
    <DialogBox ref={dialogRef} onClose={onDialogClosed}>
      <PageHeader>Create task</PageHeader>
      <FormContainer>
        <Text
          label="Title"
          {...form.register('title')}
          error={form.formState.errors.title?.message}
        />
        <MultilineText
          label="Description"
          {...form.register('description')}
          error={form.formState.errors.description?.message}
        />
        <DateInput
          label="Due Date"
          {...form.register('dueDate')}
          error={form.formState.errors.dueDate?.message}
        />
        <div className="buttons">
          <Button
            variant="action"
            buttonType="rounded"
            title="Save task"
            onClick={() => form.handleSubmit(onSubmit)()}
          >
            Save task
          </Button>
        </div>
      </FormContainer>
    </DialogBox>
  );
}

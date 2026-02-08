import styled from '@emotion/styled';
import { zodResolver } from '@hookform/resolvers/zod';
import {
  Button,
  DateInput,
  MultilineText,
  PageHeader,
  Text,
} from '@root/components';
import { theme } from '@root/theme';
import { useLayoutEffect, useRef } from 'react';
import { useForm } from 'react-hook-form';
import createTaskLogic from './create-task.logic';
import type * as model from './create-task.model';

export interface CreateTaskProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
  onTaskCreated: () => void;
}

const FormContainer = styled.div(`
    display: grid;
    grid-template-columns: 1fr 2fr;
    gap: ${theme.spaces.medium};
    &>.buttons, &>div>.error {
        grid-column: span 2;
        text-align: center;
    }
`);

export default function CreateTask(props: CreateTaskProps) {
  const { isOpen, setIsOpen, onTaskCreated } = props;
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
      dialogRef.current.showModal();
    }
  }, [isOpen]);

  function onDialogClosed() {
    if (isOpen) {
      setIsOpen(false);
    }
  }

  async function onSubmit(data: model.NewTaskData): Promise<void> {
    console.log('Raw form data', data);
    data.dueDate = data.dueDate ? new Date(data.dueDate) : undefined;
    console.log('Submitting form with data', data);
    setIsOpen(false);
    onTaskCreated();
  }

  return (
    <dialog ref={dialogRef} onClose={onDialogClosed}>
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
    </dialog>
  );
}

import { endOfDay } from 'date-fns';
import { z } from 'zod';

const createTaskSchema = z.object({
  title: z.string().min(10, 'Title must be at least 10 characters long'),
  description: z
    .string()
    .min(20, 'Description must be at least 20 characters long'),
  dueDate: z.coerce.date().min(endOfDay(new Date())).optional(),
});

export class CreateTaskLogic {
  getFormSchema() {
    return createTaskSchema;
  }
}

const createTaskLogic: CreateTaskLogic = new CreateTaskLogic();

export default createTaskLogic;

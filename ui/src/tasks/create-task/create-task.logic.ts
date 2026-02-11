import { z } from 'zod';

const createTaskSchema = z.object({
  title: z.string().min(10, 'Title must be at least 10 characters long'),
  description: z
    .string()
    .min(20, 'Description must be at least 20 characters long'),
  dueDate: z.preprocess(
    (val) => (val === '' || val == null ? undefined : val),
    z.optional(
      z
        .string()
        .regex(/^\d{4}-\d{2}-\d{2}$/, 'Invalid date format')
        .refine(
          (dateStr) => {
            const [year, month, day] = dateStr.split('-').map(Number);
            const date = new Date(year, month - 1, day);
            const today = new Date();
            today.setHours(0, 0, 0, 0);
            return date >= today;
          },
          { message: 'Due date must be today or in the future' },
        ),
    ),
  ),
});

export class CreateTaskLogic {
  getFormSchema() {
    return createTaskSchema;
  }
}

const createTaskLogic: CreateTaskLogic = new CreateTaskLogic();

export default createTaskLogic;

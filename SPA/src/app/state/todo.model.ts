import { ID } from '@datorama/akita';

export type Todo = {
  id: ID;
  title: string;
  completed: boolean;
};

export function createTodo({ id, title }: Partial<Todo>) {
  return {
    id,
    title,
    completed: false
  } as Todo;
}
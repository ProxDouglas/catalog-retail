import { api } from './api';
import type { Category, CreateCategory, UpdateCategory } from '../types';

export const categoryService = {
  getAll: () => api.get<Category[]>('/categories'),
  getById: (id: number) => api.get<Category>(`/categories/${id}`),
  create: (data: CreateCategory) => api.post<Category>('/categories', data),
  update: (id: number, data: UpdateCategory) => api.put<Category>(`/categories/${id}`, data),
  delete: (id: number) => api.delete(`/categories/${id}`),
};

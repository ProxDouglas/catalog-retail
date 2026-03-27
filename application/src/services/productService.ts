import { api } from './api';
import type { Product, CreateProduct, UpdateProduct } from '../types';

export const productService = {
  getAll: () => api.get<Product[]>('/products'),
  getActive: () => api.get<Product[]>('/products/active'),
  getById: (id: number) => api.get<Product>(`/products/${id}`),
  getByCategory: (categoryId: number) => api.get<Product[]>(`/products/category/${categoryId}`),
  create: (data: CreateProduct) => api.post<Product>('/products', data),
  update: (id: number, data: UpdateProduct) => api.put<Product>(`/products/${id}`, data),
  delete: (id: number) => api.delete(`/products/${id}`),
};

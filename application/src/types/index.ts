export interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  stock: number;
  imageUrl?: string;
  isActive: boolean;
  categoryId: number;
  categoryName: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateProduct {
  name: string;
  description?: string;
  price: number;
  stock: number;
  imageUrl?: string;
  categoryId: number;
}

export interface UpdateProduct {
  name: string;
  description?: string;
  price: number;
  stock: number;
  imageUrl?: string;
  isActive: boolean;
  categoryId: number;
}

export interface Category {
  id: number;
  name: string;
  description?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateCategory {
  name: string;
  description?: string;
}

export interface UpdateCategory {
  name: string;
  description?: string;
}

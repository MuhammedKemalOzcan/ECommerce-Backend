// src/constants/catalog.ts

import type { Category, Status } from "../types/Catalog";

export const CATEGORY_OPTIONS: readonly Category[] = [
  "All Categories",
  "Earphones",
  "Headphones",
  "Speakers",
];

export const STATUS_OPTIONS: readonly Status[] = [
  "All Status",
  "Available",
  "Out of Stock",
] as const;

export const PRODUCT_CATEGORIES = [
  "speakers",
  "headphones",
  "earphones",
] as const;

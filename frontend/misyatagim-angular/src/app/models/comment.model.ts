export interface Comment {
  productId: number;
  text: string;
  userId?: string;
  id?: string;
  username?: string; // Add username
  createdAt?: Date; // Add createdAt
}
export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
   slug:string;
   imageBase64s?: string[];
   size?:string;
   material?:string;
   color?:string;
   firmness?:string;

}
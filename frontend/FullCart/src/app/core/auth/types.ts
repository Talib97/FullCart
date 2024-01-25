
export type AuthRequest = {
  name?: string | null,
  email?: string | null,
  password?: string | null,
  confirmPassword?:string | null
}

export type AuthResponse = { accessToken: string, userInfo: AuthUser } 

type AuthUser = { id: string, name: string , roles:string[] }

export type Category = { id: string, name: string }

export type InventoryProduct = {
  id?:string
  name?: string
  description?: string
  image?: string
  price?: number
  category?: Category
  status?: Status 
  quantity?: number
  loadingStatus?:boolean
}


export type Status = {
  id?:string
  description?: string
}



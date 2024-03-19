import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './App.css';

interface Item {
  id: number;
  name: string;
}

const ItemList: React.FC = () => {
  const [items, setItems] = useState<Item[]>([]);

  const fetchItems = () => {
    axios.get('https://localhost:44313/items')
      .then(response => {
        setItems(response.data);
      });
  };

  const createItem = (name: string) => {
    axios.post('https://localhost:44313/items', { name })
      .then(() => {
        fetchItems();
      });
  };

  const deleteItem = (id: number) => {
    axios.delete(`https://localhost:44313/items/${id}`)
      .then(() => {
        fetchItems();
      });
  };

  useEffect(() => {
    fetchItems();
  }, []);

  return (
    <div>
      <input id="newItemName" />
      <button onClick={() => createItem((document.getElementById('newItemName') as HTMLInputElement).value)}>Create Item</button>
      <ul>
        {items.map(item => (
          <li key={item.id}>
            {item.name}
            <button onClick={() => deleteItem(item.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ItemList;
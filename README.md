### Functionality:

1. DB:
	1. user:
		1. id (unique)
		2. email
		3. phone
		4. username (unique)
		5. password
		6. token
		
	2. card:
		1. id (unique)
		2. owner_id
		3. is_default
		4. name
		5. card_number (unique)
		6. cvv
		7. exp_month
		8. exp_year
		
	3. transaction:
		1. id (unique)
		2. date_time
		3. from_card
		4. to_card
		5. from_user
		6. to_user
		7. amount
		8. currency

2. API:
	1. user_controller:
		1. register/{username}/{email}/{phone}/{password}
		2. login/{username}/{password}
		3. change_phone/{token}/{new_phone}
		
	2. card_controller:
		1. add/{token}/{number}/{month_exp}/{year_exp}/{cvv}
		2. remove/{token}/{card_id}
		3. cards/{token}
		4. rename/{token}/{card_id}/{new_name}
		5. set_default/{card_id}
		6. get_cvv/{card_id}
		7. get_date/{card_id}
		8. get_num/{card_id}
		
	3. transaction_controller:
		1. send/by_card_num/{token}/{from_card_id}/{to_card_num}/{amount}/{currency}
		2. send/by_card_id/{token}/{from_card_id}/{to_card_id}/{amount}/{currency}
		
3. Frontend
	1. login/register menu
	2. main_menu:
		1. settings
		2. profile
		3. list_of_cards
		4. send_money
		
		
		
		
		
		
		
		